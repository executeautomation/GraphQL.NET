#!/usr/bin/env sh

set -e
set -x

cd "$(dirname "${0}")/.."

export COMPOSE_HTTP_TIMEOUT=200

docker-compose build

docker-compose up -d graphql_api
docker-compose up --no-deps nodejs

exit_code=$(docker inspect nodejs -f '{{ .State.ExitCode }}')

if [ $exit_code -eq 0 ]; then
    exit $exit_code
else
    echo "Test failed"
fi

