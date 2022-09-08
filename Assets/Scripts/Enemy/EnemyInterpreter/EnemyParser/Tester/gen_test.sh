#!/bin/bash

files=`ls ..`
for f in $files ; do
    case $f in *\.cs)
        filename=`basename $f .cs`
        testername="${filename}Tester.cs"
        if [ ! -e $filename ] ; then
            `touch ${filename}Tester.cs`
            echo "using System;
public partial class EnemyASTNodeTester
{
}
" > $testername
        fi
        ;;
    *)
    esac
done
