cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "EX55Y55D35P.pad"
CALL :checkfile "ths45x55x45x10.fsm"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "2-87215-3.psm"

exit

:checkfile
@echo off
dir %1 1>nul 2>nul
if errorlevel 1 goto checkfile_err
goto end
:checkfile_err
echo Expected file %1 not found
pause > nul
exit
:end
@echo on
