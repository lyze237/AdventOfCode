#!/bin/bash

function net() {
    if hash dotnet 2>/dev/null; then
        dotnet "$@"
    else
        dotnet.exe "$@"
    fi
}

date=$1

re='^[0-9]+$'
if ! [[ $date =~ $re ]] ; then
   echo "error: Not a number" >&2; exit 1
fi

echo Creating Day$date


echo Creating directory Day$date
mkdir Day$date

echo Creating dotnet classlib
cd Day$date
net new classlib 
cd ..

echo Adding new project to solution
net sln add Day$date/Day$date.csproj

echo Copying challenge files to project
rm Day$date/Class1.cs
cp template/Challenge1.cs Day$date/Day${date}Challenge1.cs
cp template/Challenge2.cs Day$date/Day${date}Challenge2.cs
echo Insert input here > Day$date/input
echo Replacing template file content with actual date
sed -i -- "s/DayX/Day${date}/g" Day$date/Day${date}Challenge*.cs

echo Adding referneces to proper projects
net add Day$date/Day$date.csproj reference Utils/Utils.csproj
net add AdventOfCode/AdventOfCode.csproj reference Day$date/Day$date.csproj

