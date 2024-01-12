@echo Off

set para=/FFT /R:1 /W:1 /XD .git .gitignore .vs .vscode node_modules dist bin obj /XF *.deploy *.user *.suo *.manifest *.pdb *.vshost.exe* EntityFramework*.xml *.application /NJH /NJS /NDL 

@echo.
@echo.
@echo.
@echo    robocopy    C:\Users\alexp\source\repos\alex-pi    C:\g\alex-pi\Src   ...
@echo.
@echo.
    
@REM robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /MIR   /XF UserMap.json    :DELETES!!! if new file is in the wrong place: source
robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%       /S /XO /XF UserMap.json

:menu
@echo.
@echo.
choice /C 12 /N /M "1. MIR / Delete!!!    2. Exit? "
if errorlevel 2 goto exit
if errorlevel 1 goto open
goto menu

:open
robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%  /MIR   /XF UserMap.json    
goto end

:exit
goto end

:end
@echo.
@echo.
@echo.
@REM @echo      ******************* The End ***********************
set para=

timeout 260