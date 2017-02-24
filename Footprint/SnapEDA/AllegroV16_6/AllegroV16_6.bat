cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX28Y13D0T.pad"
CALL :checkfile "RX52Y13D0T.pad"
CALL :checkfile "RX40Y13D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "SOT-666.psm"
CALL :checkfile "SOT-666-M.psm"
CALL :checkfile "SOT-666-L.psm"

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
