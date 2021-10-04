cd ClientApp
call npm install
node --max_old_space_size=8192 node_modules/@angular/cli/bin/ng build --configuration production ---progress true
cd ../
robocopy ClientApp/dist wwwroot/app /S /XD .svn /XF *.scss  *.cs *.pdb *.map *.user *.csproj /NFL /NJH /NP 