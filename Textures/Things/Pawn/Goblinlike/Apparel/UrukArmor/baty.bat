@echo off
Setlocal enabledelayedexpansion

Set "Pattern=FeMale"
Set "Replace=Female"

For %%# in ("C:\Program Files (x86)\Steam\steamapps\common\RimWorld\ModsWorkspace\Lord-of-the-Rims---Orcs-and-Goblins\Textures\Things\Pawn\Goblinlike\Apparel\UrukArmor\*.png") Do (
    Set "File=%%~nx#"
    Ren "%%#" "!File:%Pattern%=%Replace%!"
)

Pause&Exit