cls
echo off
set mydate=%date:~10,4%%date:~4,2%%date:~7,2%
set captime=%TIME%
set h=%captime:~0,2%
set m=%captime:~3,2%
set mytime=%h%%m%
set dumpfile=backup.hamradio.%mydate%.%mytime%.sql
echo %dumpfile%
"C:\Program Files (x86)\MySQL\MySQL Server 5.7\bin\mysqldump.exe" -u root -ppassword hamradio >%dumpfile%