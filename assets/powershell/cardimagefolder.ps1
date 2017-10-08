$FolderToCreate = $(AppSettings.cardimagefolderpath)

if (!(Test-Path $FolderToCreate -PathType Container)) {
    New-Item -ItemType Directory -Force -Path $FolderToCreate
}