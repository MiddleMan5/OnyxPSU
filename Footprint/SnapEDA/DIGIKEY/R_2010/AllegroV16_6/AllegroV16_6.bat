cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX44Y102D0T.pad"
CALL :checkfile "RX52Y106D0T.pad"
CALL :checkfile "RX36Y102D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "STA_CSRN2010.psm"
CALL :checkfile "STA_CSRN2010-M.psm"
CALL :checkfile "STA_CSRN2010-L.psm"

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
