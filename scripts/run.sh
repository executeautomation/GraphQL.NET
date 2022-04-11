#!/usr/bin/env s
docker-compose build

docker-compose up -d graphql_api
docker-compose up --no-deps newman

exit_code=$(docker inspect newmantest -f '{{ .State.ExitCode }}')

if [ $exit_code -eq 0 ]; then
    exit $exit_code
else
    echo "Test failed"
fi
