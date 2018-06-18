( User Vocabulary )

require ./runtime.fs
require ./vocabulary.fs
require ./cartridge.fs
require ./header.fs
require ./asm.fs
require ./rom.fs
require ./compiler/cross.fs

: export
  parse-name
  2dup find-name name>int >r
  nextname r> alias ;

: [user-definitions]
  get-current
  also gbforth-user definitions ;

: [end-user-definitions]
  previous set-current ;


[user-definitions]
also gbforth

: [host] also forth ; immediate
: [endhost] previous ; immediate

: ! rom! ;
: c! romc! ;
: , rom, ;
: c, romc, ;
: s" rom" ;

export ( immediate
export \ immediate
export ==>
export include
export require
export [asm]
export [endasm]

export main:

export title:
export gamecode:
export makercode:

export code
export -end-code immediate
export end-code immediate

export dup
export drop
export +
export *
export or

[asm]
: execute # call, ;
[endasm]

: constant
  >r
  parse-next-name
  2dup nextname r@ constant

  nextname
  r> xconstant ;

: here rom-offset ;
: unused rom-size here - ;
: create here constant ;
: cells $2 * ;
: allot rom-offset+! ;

: variable
  CP @
  $2 CP +!
  constant ;

: ' x' ;
: ] x] ;
: ]L xliteral x] ;
: :noname x:noname ;

: : x: ;

previous
[end-user-definitions]
