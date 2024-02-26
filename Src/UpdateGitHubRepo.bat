@echo Off

set para=/FFT /R:1 /W:1 /XD .git .gitignore .vs .vscode node_modules dist bin obj /XF *.deploy *.user *.suo *.manifest *.pdb *.vshost.exe* EntityFramework*.xml *.application /NJH /NJS /NDL 
set say=%OneDrive%\Public\bin\say.exe
    
@REM robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /MIR       :DELETES!!! if new file is in the wrong place: source

call :1

:menu
start /min %say% "hm?"
echo.
echo [99;94m   Normal direction:: [0m 
robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%       /S /XO   /L
echo.
echo.
echo [99;94m   Reverse direction:: [0m 
robocopy C:\g\alex-pi\Src C:\Users\alexp\source\repos\alex-pi %para%       /S /XO   /L
echo.
echo.
echo     Select:  [99;92m   Norm    Revr   Palette   Quit [0m      [99;93m   ... or auto quitting in 99 sec ... [0m 
choice /T 99 /C nrpq /N /D q
echo [99;94m   selected  %ERRORLEVEL%: [0m 
call :%ERRORLEVEL%
timeout /T 1 >Nul
goto menu

:1 Normal
  robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /S /XO 
goto :EOF
:2 Reverse
  robocopy C:\g\alex-pi\Src C:\Users\alexp\source\repos\alex-pi %para%  /S /XO 
goto :EOF
:3 Palette
  call :Plt
goto :EOF
:4
goto end
:5
:6
:7
:8
:9
:10
  start /min %say% %ERRORLEVEL% - selected 
goto :EOF

:Plt
  for /L %%i in (40,1,47)   do   echo   [%%i;39m [30m 30m [31m 31m [32m 32m [33m 33m [34m 34m [35m 35m [36m 36m [37m 37m [%%i;99m [90m 90m [91m 91m [92m 92m [93m 93m [94m 94m [95m 95m [96m 96m [97m 97m [0m   bg-%%i
  for /L %%i in (100,1,107) do   echo   [%%i;39m [30m 30m [31m 31m [32m 32m [33m 33m [34m 34m [35m 35m [36m 36m [37m 37m [%%i;99m [90m 90m [91m 91m [92m 92m [93m 93m [94m 94m [95m 95m [96m 96m [97m 97m [0m   bg-%%i
goto :EOF

:end
  echo   Bye
  echo   Bye
  echo   Bye
  start /min %say% "Bye"
  set para=
  set say=
  rem timeout %ERRORLEVEL%
exit