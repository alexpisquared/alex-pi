@echo Off

set para=/FFT /R:1 /W:1 /XD .git .gitignore .vs .vscode node_modules dist bin obj /XF *.deploy *.user *.suo *.manifest *.pdb *.vshost.exe* EntityFramework*.xml *.application /NJH /NJS /NDL 
set say=%OneDrive%\Public\bin\say.exe

@echo.
@echo.
@echo.
@echo    robocopy    C:\Users\alexp\source\repos\alex-pi    C:\g\alex-pi\Src   ...
@echo.
@echo.
    
@REM robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /MIR   /XF UserMap.json    :DELETES!!! if new file is in the wrong place: source
robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%       /S /XO /XF UserMap.json  /L

:menu
start /min %say% "hm?"
@echo.
echo [99;92m   Redo    MIR    Quit [0m 
echo [99;92m   Redo    MIR    Quit [0m 
echo [99;92m   Redo    MIR    Quit [0m 
@echo.
echo [99;93m   Auto quitting in 9 sec [0m 
@echo.
choice /T 9 /C 1rm4q /N /D q
echo [99;94m   selected  %ERRORLEVEL% [0m 
start /min %say% selected  %ERRORLEVEL%
call :%ERRORLEVEL%
start /min %say% "Review for 2 seconds"
timeout /T 2 >Nul
goto menu

:10
:9
:8
:7
:6
start /min %say% %ERRORLEVEL% - selected 
:5
goto end
:4
goto :EOF
:3
  robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /MIR   /XF UserMap.json    
goto :EOF
:2
:1
  robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /S /XO /XF UserMap.json
goto :EOF

:end
@echo.
@echo.
@echo.
echo      [98;91m   ******************* The End *********************** [0m 
echo      [97;92m   ******************* The End *********************** [0m 
echo      [96;93m   ******************* The End *********************** [0m 
echo      [96;94m   ******************* The End *********************** [0m 
echo      [95;93m   ******************* The End *********************** [0m 
set para=
set say=
timeout %ERRORLEVEL%
exit