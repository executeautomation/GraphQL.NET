#!/usr/bin/env bash

ls -l 
#install newman
npm install -g newman
#Run newman collection and environment
newman run ./postman/collection.json -e ./postman/environment.json --reporters cli --insecure


if [ $? -eq 0 ] 
then 
  echo "Successfully executed newman test" 
  exit
else 
  echo "Failing executing newman test" >&2 
  exit
fi


