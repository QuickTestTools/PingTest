cd /d src/PingTest
bflat build --no-debug-info --os=windows --arch=x64
move PingTest.exe bin/PingTest_win_x64.exe
bflat build --no-debug-info --os=windows --arch=arm64
move PingTest.exe bin/PingTest_win_arm64.exe
bflat build --no-debug-info --os=linux --arch=x64
move PingTest bin/PingTest_linux_x64
bflat build --no-debug-info --os=linux --arch=arm64
move PingTest bin/PingTest_linux_arm64
cd bin
