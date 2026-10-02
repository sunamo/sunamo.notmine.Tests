---
schema_version: 6
type: tests
file_count: 18
avg_lines_per_file: 28
move_to_legacy_percent: 20
generated_date: 2026-10-01
generated_time: 16:43:13
github_source_url: 
last_build_ok: 
last_build_date: 
last_tests_run_date: 
covered_lines: 
total_lines: 
---

## Description

Sbírka testovacích projektů (Dates.Tests, UAParser.Tests, Metaproject.PackageIndex.Functions.ParseCsprojFile.Tests) pro knihovny, jejichž zdrojové projekty dříve žily v sunamo.notmine a postupně se stěhovaly do pinp/wnp. Originály zdrojů autor smazal, tyto kopie testů jsou jediné dochované.

## Původ zdrojáků

Staženo z GitHubu: **ne** — vlastní projekt.

- Ověřeno: Autorský kód balíčku Sunamo, bez cizího původu v remote ani v metadatech.

## Doporučení přesunu do legacy

Doporučení přesunu do sunamocz-legacy.visualstudio.com: **20 %** — Obsolete sbírka testů, ale uživatel rozhodl, že se zachová.

- Jediné dochované kopie testů
- Uživatel nařídil nepřesouvat ani nepřesouvat

## Vazby na moje repa

- Submoduly: žádné
- ProjectReference / PackageReference: `SunamoShared` (PackageReference), `Metaproject.PackageIndex.Functions.ParseCsprojFile` (ProjectReference, cíl chybí), `Metaproject.PackageIndex.Structures.PackageProject` (ProjectReference, cíl chybí), `UAParser` (ProjectReference, cíl chybí)
