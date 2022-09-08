# Chrono

https://dev.azure.com/AlexPi/_git/alex-pi?_a=history

## 2018-05
### Creating PWA with Angular 6 using Angular Material
www.youtube.com/watch?v=0UKJbtdPx4I

cd .\ng0poc\
npm install -g @angular/cli
ng --version
ng new my-pwa  --style=scss  --routing
cd .\my-pwa\
code .
ng serve 
ng add      @angular/material
ng generate @angular/material:material-nav    --name="app-nav"
ng generate @angular/material:material-table  --name="app-table"
ng add      @angular/pwa

npm install lite-server --save-dev
add to package.json: "start:prod": "ng build --prod && lite-server --baseDir dist/pwa-on20190904",
npm run start:prod

ng build --base-href "/xApi/" --prod
robocopy D:\gh\x\ng0poc\my-pwa\dist\my-pwa\ \\nes-corp\wwwroot\xApi\ -mir


## 2020-12 - Angular 10 Upgrades are Still Bad News!!!!! - John Peters - 2020-10-27 - https://dev.to/jwp/angular-upgrades-are-bad-news-21l2
## 2020-12-14  Upgrading NG from 8 to 11 - globally and locally - succeeded!!

## 2021-01
Guestbook - added visuals; need talk to SQL DB.
Prettier - Removed to avoid:
  1. auto-double-quoting
  2. 80 max length

## 2022-09-07
Updating to ng14 following this:  https://update.angular.io/?l=3&v=13.0-14.0

BTW, the build on Azure: https://dev.azure.com/AlexPi/alex-pi/_build