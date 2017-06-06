

SET ext=.txt
SET base=teste

:start
set /a var+=1
if %var% EQU 11 goto end
  SET "filename=%base%+%var%+%ext%"
  ECHO ON
  echo %var%
  start IIA3.exe -batchmode -tournament=true -cuts=3 -seed=%var% -level=2 -generations=50 -probm=0.1 -probc=0.1 -log=%filename% -tsize=5 -elitism=5
goto start
:end
exit
