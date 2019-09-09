require ./gbhw.fs
require ./term.fs

: scroll-to-bottom ( -- )
  cursor-y @ 8 *
  [ SCRN_Y 8 - ]L - \ SCRN_VY mod
  rSCY c! ;

: type-fill-line ( addr u -- )
  type
  SCRN_VX_B cursor-x @ - spaces ;

:m log"
  postpone scroll-to-bottom
  postpone s"
  postpone type-fill-line
  postpone cr
; immediate

: init-logger
  init-term
  scroll-to-bottom ;
