---
schema_version: 11
type: tests
category_override: none
file_count: 24
file_extensions: cs:27, csproj:8, config:5, noext:3, old:3, bigram_freqs:2, bigrams:2, jsonanddelete:2, md:2, numbers:2, punc:2, slnx:2, tif:2, training_text:2, unicharambigs:2, unigram_freqs:2, wordlist:2, txt:1
file_extensions_updated: 2026-10-04
avg_lines_per_file: 51
total_lines: 974
metrics_lm: 2026-10-04 16:02:36
move_to_legacy_percent: 15
description_updated: 2026-10-04
links_updated: 2026-10-04
github_source_url: not run
origin_status: failed
origin_checked: 2026-10-04
article_source_url: not run
article_status: pending
article_checked: not run
last_build_ok: not run
last_build_date: not run
last_tests_run_date: not run
covered_lines: not run
---

## Description

Testovací projekty ke zdrojům převzatým z cizích knihoven, hlavně pro FubuCsProjFile, SlnGen.Common a podpůrnou knihovnu FubuTestingSupport. Součástí je i projekt Runner pro spuštění. Dále obsahuje testy Dates.Tests, UAParser.Tests, Tesseract.Tests a Metaproject.PackageIndex.Functions.ParseCsprojFile.Tests. Originály některých zdrojů autor smazal, tyto kopie testů jsou jediné dochované. Řešení je sunamo.notmine.unknownoriginal.Tests.slnx.

## Původ zdrojáků

Staženo z GitHubu: **nezjištěno** — pokus o ověření původu selhal.

- Ověřeno: Složka nemá git, názvem a obsahem jde o kopie cizích testů.

## Doporučení přesunu do legacy

Doporučení přesunu do sunamocz-legacy.visualstudio.com: **15 %** — Jediné zachované kopie testů, nesmí se mazat.

- Originály testů už autor smazal.

## Vazby na moje repa

- Submoduly: žádné
- ProjectReference / PackageReference: `SunamoShared` (PackageReference)
