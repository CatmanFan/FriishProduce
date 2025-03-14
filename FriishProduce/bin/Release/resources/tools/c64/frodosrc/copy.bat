@echo off
cd /d %~dp0
echo F|xcopy /s /q /y /f "1541 ROM" "C:\1541 ROM"
echo F|xcopy /s /q /y /f "Basic ROM" "C:\Basic ROM"
echo F|xcopy /s /q /y /f "Char ROM" "C:\Char ROM"
echo F|xcopy /s /q /y /f "Kernal ROM" "C:\Kernal ROM"