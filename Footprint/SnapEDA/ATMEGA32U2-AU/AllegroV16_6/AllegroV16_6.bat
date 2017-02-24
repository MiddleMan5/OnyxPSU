cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX9Y46D0T.pad"
CALL :checkfile "RX9Y70D0T.pad"
CALL :checkfile "RX9Y58D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "32A.psm"
CALL :checkfile "32A-M.psm"
CALL :checkfile "32A-L.psm"

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
