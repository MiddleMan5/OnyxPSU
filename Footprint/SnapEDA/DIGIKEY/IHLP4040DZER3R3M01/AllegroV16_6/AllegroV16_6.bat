cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX131Y128D0T.pad"
CALL :checkfile "RX100Y128D0T.pad"
CALL :checkfile "RX115Y128D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "IND_IHLP-4040DZ.psm"
CALL :checkfile "IND_IHLP-4040DZ-M.psm"
CALL :checkfile "IND_IHLP-4040DZ-L.psm"

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
