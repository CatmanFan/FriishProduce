del ..\bin\Debug\locales\* /s/q
del ..\bin\Debug\resources\tools\banners\* /s/q
xcopy %cd% ..\bin\Debug\locales\ /E/C/I
cd ..\Resources\banners\
xcopy %cd% ..\..\bin\Debug\resources\tools\banners\ /E/C/I