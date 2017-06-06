SET new=C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\testebase\
SET end=\.
SET temp=m1c1
SET "filename=%new%%temp%%end%"

ECHO %filename%
pause
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+1+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+2+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+3+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+4+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+5+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+6+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+7+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+8+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+9+.txt %filename%
xcopy C:\Users\Gabriel\Documents\ProjetoIIA_3\TP3\teste+10+.txt %filename%
calc %temp%
pause