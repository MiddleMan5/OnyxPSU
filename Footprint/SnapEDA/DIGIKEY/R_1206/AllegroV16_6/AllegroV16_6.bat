cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX24Y75D0T.pad"
CALL :checkfile "RX16Y71D0T.pad"
CALL :checkfile "RX20Y71D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "C1206C.psm"
CALL :checkfile "C1206C-M.psm"
CALL :checkfile "C1206C-L.psm"

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
