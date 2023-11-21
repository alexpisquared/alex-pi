@echo Off

set para=/FFT /R:1 /W:1 /XD .git .gitignore .vs .vscode node_modules dist bin obj /XF *.deploy *.user *.suo *.manifest *.pdb *.vshost.exe* EntityFramework*.xml *.application /NJH /NJS /NDL 

@echo.
@echo.
@echo.
@echo    robocopy    C:\Users\alexp\source\repos\alex-pi    C:\g\alex-pi\Src   ...
@echo.
@echo.
    
robocopy C:\Users\alexp\source\repos\alex-pi C:\g\alex-pi\Src %para%       /MIR  /XF UserMap.json
@echo.
@echo.
@echo.
@REM @echo      ******************* The End ***********************
set para=

timeout 26