require ./utils/bytes.fs
require ./utils/strings.fs

: to-symbol-file ( c-addr u -- c-addr' u' )
  s" .sym" replace-ext ;

0 Value sym-out

: set-sym-file
  w/o create-file throw to sym-out ;

: sym-emit
  sym-out if
    sym-out emit-file throw
  else
    drop
  then ;

: sym-write ( c-addr u -- )
  sym-out if
    sym-out write-file throw
    sym-out flush-file throw
  else
    2drop
  then ;

: sym-cr
  #10 sym-emit ;

: addr-to-str ( nn -- c-addr u )
  base @ >r
  hex
  0
  <<#
  # # # #
  #>
  #>>
  r> BASE ! ;

: sym ( c-addr u nn -- )
  s" 00:"       sym-write
  addr-to-str   sym-write
  s"  "         sym-write
  ( c-addr u )  sym-write
  sym-cr
;
