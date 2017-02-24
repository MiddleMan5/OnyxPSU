cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX66p93Y84p65D0T.pad"
CALL :checkfile "EX33p46Y11p02D0T.pad"
CALL :checkfile "RX33p46Y11p02D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "DSC10_1P70X2P15.psm"

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
