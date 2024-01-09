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

@echo.
@echo.
@echo.
@REM @echo      ******************* The End ***********************
set para=

timeout 260