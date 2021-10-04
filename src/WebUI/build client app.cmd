cd %1
RMDIR "wwwroot/app" /S /Q

call ~build-app

IF %ERRORLEVEL% LSS 8 goto finish
goto :eof
:finish
echo All Done.
exit /b 0