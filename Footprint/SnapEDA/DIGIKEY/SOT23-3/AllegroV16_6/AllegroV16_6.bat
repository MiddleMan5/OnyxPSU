cd %~dp0
allegro -s CustomShapes.scr
pad_designer -s AllegroV16_6_pads.scr
CALL :checkfile "RX22Y59p71D0T.pad"
CALL :checkfile "RX24Y64D0T.pad"
CALL :checkfile "RX24Y71p71D0T.pad"
CALL :checkfile "RX20Y40D0T.pad"
CALL :checkfile "RX20Y47p71D0T.pad"
CALL :checkfile "RX22Y52D0T.pad"
allegro -s AllegroV16_6_brd.scr
CALL :checkfile "SOT23.psm"
CALL :checkfile "SOT23-M.psm"
CALL :checkfile "SOT23-L.psm"

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
