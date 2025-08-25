#!/bin/bash

set -euo pipefail

api="http://localhost:8080/api/concepts"

jq -c '.[]' concept.json | while read -r concept; do
  curl -v -X POST "$api" \
    -H "Content-Type: application/json" \
    -d "$concept"
done
