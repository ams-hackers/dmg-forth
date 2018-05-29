require ./rom.fs
require ./kernel.fs

also gb-assembler

( Cross words )

1 constant F_IMMEDIATE

struct
  cell% field xname-flags
  cell% field xname-addr
end-struct xname%

: make-xname ( addr flag -- xname )
  >r >r
  xname% %allot
  r> over xname-addr !
  r> over xname-flags ! ;

: >xcode xname-addr @ ;
: >xflags xname-flags @ ;
: ximmediate? >xflags F_IMMEDIATE and 0<> ;


( Cross Dictionary )

wordlist constant xwordlist

: create-xname ( addr flag -- )
  get-current >r
  xwordlist set-current
  make-xname create ,
  r> set-current ;

\ Create a cross-word, reading its name from the input stream using
\ `create-xname`.
: xcreate
  rom-offset 0 create-xname ;

: ximmediate-as
  latest name>int F_IMMEDIATE create-xname ;

: find-xname ( addr u -- xname )
  2>r
  get-order
  xwordlist 1 set-order
  2r>

  find-name dup if name>int >body @ then

  >r
  set-order
  r> ;

\ for debugging
: xwords
  xwordlist >order words previous ;



( Cross Compiler )

\ Read the next word available in the inputs stream. Automatically
\ refill the stream if needed.
: parse-next-name
  parse-name dup if
  else
    refill if 2drop recurse then
  then ;

: process-xname ( xname -- )
  dup ximmediate? if >xcode execute else >xcode xcompile, then ;

: process-number ( n -- )
  xliteral, ;

: process-word ( addr u -- )
  2dup find-xname ?dup if
    nip nip process-xname
  else
    s>number? if
      drop process-number
    else
      -1 abort" Unknown word"
    then
  then ;


( 0 if we the host is interpreting words,
 -1 if we are compiling into the target )
variable xstate

\   call:     [11 001 101] [nnnnnnnn] | 205        -#10 -$A
\   jp:       [11 000 011] [nnnnnnnn] | 195
\   call cc:  [11 0cc 100] [nnnnnnnn] | 196..220   -2
\   jp cc:    [11 0cc 010] [nnnnnnnn] | 194..218
: is-call? %11001101 = ; \ $CD
: is-cond-call? %11100111 and %11000100 = ; \ $C4, $CC, $D4, $DC

: optimise-tail-call
  rom-offset 3 - dup \ get opcode offset
  rom@  \ get opcode value
  dup is-call? if
    $A - \ change to JP
    swap \ val addr -- addr val
    rom!
  else
    dup is-cond-call? if
      $2 - \ change to JP cc
      swap
      rom!
    else
      drop drop
  then then ;

: x[ 0 xstate ! ; ximmediate-as [
: x; x[ optimise-tail-call xreturn, ; ximmediate-as ;

: x]
  1 xstate !
  begin
    parse-next-name
    process-word
    xstate @ while
  repeat ;

: x'
  parse-next-name find-xname ?dup if >xcode else -1 abort" Unknown word " then ;

: x:
  rom-offset >r
  parse-next-name 2>r
  x]
  2r> nextname
  r> 0 create-xname ;

previous
