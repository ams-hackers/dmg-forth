( User Vocabulary )

require ./cartridge.fs
require ./cross.fs

: export
  parse-name 
  2dup find-name name>int >r
  nextname r> alias ;

: [user-definitions]
  get-current
  also dmgforth-user definitions ;
  
: [end-user-definitions]
  previous set-current ;


[user-definitions]
also dmgforth

: [host] also forth ;
: [endhost] previous ;

: c, rom, ;

export ( 
export ==>
export \
export also
export constant
export gb-assembler
export include
export previous
export require

export main
export title:

( TODO: Remove me! )
export ps-clear,
export ps-push-lit,

: ' x' ;

previous
[end-user-definitions]
