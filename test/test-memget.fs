also gb-assembler

$150 ==>
label main

ps-clear,

$0104 ps-push-lit,
' c@ # call,
$0105 ps-push-lit,
' c@ # call,
$0106 ps-push-lit,
' c@ # call,

label loop
halt,
loop jr,

previous
