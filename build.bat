cd /d src/PingTest
dotnet build -c Release
rd /s /Q obj
bflat build --os=windows --arch=x64 --no-debug-info --reference "./bin/Release/Quick.Localize.dll"
move PingTest.exe bin/PingTest_win_x64.exe
bflat build --os=windows --arch=arm64 --no-debug-info --reference "bin/Release/Quick.Localize.dll"
move PingTest.exe bin/PingTest_win_arm64.exe
bflat build --os=linux --arch=x64 --no-debug-info --reference "bin/Release/Quick.Localize.dll"
move PingTest bin/PingTest_linux_x64
bflat build --os=linux --arch=arm64 --no-debug-info --reference "bin/Release/Quick.Localize.dll"
move PingTest bin/PingTest_linux_arm64
cd bin
