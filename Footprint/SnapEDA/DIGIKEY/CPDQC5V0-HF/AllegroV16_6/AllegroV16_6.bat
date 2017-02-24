cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX20Y22D0T.pad"
CALL :checkfile "RX24Y22D0T.pad"
CALL :checkfile "RX20Y22D0TSM2.pad"
CALL :checkfile "RX28Y22D0T.pad"
CALL :checkfile "RX28Y22D0TSM2.pad"
CALL :checkfile "RX24Y22D0TSM2.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "CPDQC.psm"
CALL :checkfile "CPDQC-M.psm"
CALL :checkfile "CPDQC-L.psm"

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
