#!/usr/bin/env bash
cd `dirname $0`
# pwd
# echo $1
mono src/examples/repl/bin/Debug/schemy.repl.exe $1
