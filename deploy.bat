cd .nuget
nuget Update -self
set nuget=%cd%\nuget
del *.old
cd ..
cd TreeDotNet
FOR %%f IN (*.csproj) DO (
	%nuget% pack %%f -Prop Configuration=Release
)
FOR %%f IN (*.nupkg) DO (
	%nuget% push %%f
)
del *.nupkg
cd ..
