rmdir -Force -Recurse workspace

mkdir workspace\build
mkdir workspace\content
mkdir workspace\lib\net45
mkdir workspace\tools

copy .\Package.nuspec .\workspace\.

copy ..\output\ClusterManager.dll .\workspace\lib\net45\
copy ..\output\ExtensionForVS.vsix .\workspace\tools\
copy ..\output\GuntherTesterRunner.exe .\workspace\tools\
copy -Recurse ..\ExtensionForVS\ .\workspace\build\

.\nuget.exe pack .\workspace\Package.nuspec -Prop Configuration=Release