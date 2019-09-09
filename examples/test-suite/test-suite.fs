title: ANSTESTSUITE
gamecode: ANSF

require ibm-font.fs
require logger.fs
require tester.fs

variable #total
: }T 1 #total +! }T ;

: run-tests
  log" Dup"
  T{ 1 2 dup -> 1 2 2 }T
  T{ -5 dup dup -> -5 -5 -5 }T

  log" Swap"
  T{ 1 2 3 swap -> 1 3 2 }T
  T{ 1 2 3 swap swap -> 1 2 3 }T

;

: main
  install-font
  init-logger
  init-tester
  0 #total !
  log" Starting tests..."
  run-tests
  log" --------------------"
  #total @ 2 .r space log" Total"
  #total @ #errors @ - 2 .r space log" Passed"
  #errors @ 2 .r space log" Failed"  ;
