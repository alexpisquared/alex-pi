## 2024-07-14

See # Done so far: mark below.

To fix these vulnerabilities, follow these step-by-step instructions:

1. Backup your project:
   - Create a copy of your project directory
   - Commit all current changes to your version control system

2. Update npm and node:
   ```
   npm install -g npm@latest
   nvm install --lts
   ```

3. Update Angular CLI:
   ```
   npm uninstall -g @angular/cli
   npm cache clean --force
   npm install -g @angular/cli@latest
   ```

4. In your project directory, update Angular packages:
   ```
   ng update @angular/core @angular/cli
   ```
# Done so far:
# 5. Remove vulnerable packages:
   ```
   npm uninstall axios localtunnel browser-sync lite-server request webdriver-manager protractor tough-cookie xml2js selenium-webdriver webdriver-js-extender
   ```

6. Install latest versions of necessary packages:
   ```
   npm install axios@latest localtunnel@latest browser-sync@latest lite-server@latest tough-cookie@latest xml2js@latest
   ```

7. For testing, consider replacing Protractor with a modern alternative like Cypress or Playwright:
   ```
   npm install --save-dev cypress
   ```
   or
   ```
   npm install --save-dev @playwright/test
   ```

8. Update your package.json to remove any references to the removed packages

9. Update your test scripts and configurations to use the new testing framework

10. Run npm audit fix to address any remaining issues:
    ```
    npm audit fix
    ```

11. If there are still vulnerabilities, try:
    ```
    npm audit fix --force
    ```
    Note: This may cause breaking changes, so test thoroughly afterwards

12. Review your application thoroughly to ensure it still functions as expected

13. Run your test suite to catch any breaking changes

14. If everything works, commit your changes:
    ```
    git add .
    git commit -m "Fixed vulnerabilities and updated packages"
    ```

15. If you encounter any issues, consult the documentation for each updated package for migration guides

Remember, forcing updates can sometimes lead to breaking changes. Always test your application thoroughly after making these changes. If you encounter issues, you may need to address them one by one, potentially rolling back some changes and finding alternative solutions.
