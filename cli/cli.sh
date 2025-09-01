#!/bin/bash

set -euo pipefail

API="localhost:8080/api/concepts"



main(){
  local cmd=$1
  shift

  case "$cmd" in
    concept)
      concept "$@"
      ;;
    *)
      echo "Unknown command: $cmd"
      exit 1
      ;;
  esac
}

concept(){
  local action=$1
  shift

  case "$action" in
    list)
      curl -s $API | jq -r '.[] | "\(.id)\t\(.name)\t\(.description)"' | column -t -s $'\t'
      ;;
    add)
      local name=$1
      local desc=$2
      local payload=$(jq -n \
        --arg name "$name" \
        --arg desc "$desc" \
        '{name:$name, description:$desc, dependencies:[]}')

      curl -sX POST $API \
        -H "Content-Type: application/json" \
        -d "$payload" | jq
  esac


}




main "$@"
