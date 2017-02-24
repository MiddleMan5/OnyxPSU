cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX52Y109D0T.pad"
CALL :checkfile "RX36Y105D0T.pad"
CALL :checkfile "RX44Y105D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "S41.psm"
CALL :checkfile "S41-M.psm"
CALL :checkfile "S41-L.psm"

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
