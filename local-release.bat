@echo off
CLS

SET feed="C:\\Workenv\\Tools\\Nuget\\feed\\"
SET prev=%feed%"..\\previous_versions\\"

IF NOT EXIST %feed% MKDIR %feed%
IF NOT EXIST %prev% MKDIR %prev%

MOVE /Y %feed%"NODUS.1*" %prev%

%CD%\Tools\NuGet.exe pack "NODUS/NODUS.csproj" -Build -Symbols -Verbosity normal -Properties Configuration=Release -OutputDirectory %feed%

pause