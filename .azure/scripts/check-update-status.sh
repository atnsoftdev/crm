#! /bin/bash -e

target=$(echo "${BUILD_TARGET:-$1}" | tr '[:upper:]' '[:lower:]')
subpath="${SUBPATH:-$2}"

if [[ "$target" == "" ]] || [[ "$subpath" == "" ]]; then
    echo "Usage: $0 [<build target> [<subpath>]]"
    exit 1
fi

updated='false'

if [[  "$(git show --name-only --format=%n -- "$subpath" | tr -d '\n')" != "" ]]; then
    updated='true'
fi

echo "The $target project is $([[ "$updated" == 'true' ]] && echo 'updated' || echo 'not updated') since the last build."

for setter in "##vso[task.setvariable variable=updated]$updated" "##vso[task.setvariable variable=updated;isOutput=true]$updated"; do
  echo "$setter"
  echo "'$setter"
done