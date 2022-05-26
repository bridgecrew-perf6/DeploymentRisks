#!/bin/bash
set -a
source process.env

export buildDate=$(date '+%Y-%m-%d_%H:%M:%S') 
export gitcommithash=$(git rev-parse HEAD) 
docker compose build
docker compose up