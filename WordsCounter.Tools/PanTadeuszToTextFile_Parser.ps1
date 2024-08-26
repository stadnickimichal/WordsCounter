$url = "https://wolnelektury.pl/media/book/txt/pan-tadeusz.txt"
$currentPath = Split-Path $psise.CurrentFile.FullPath
$outputDirectory = Join-Path $currentPath "..\WordsCounter.PerformanceTest\TestFiles"

$requestResault = Invoke-WebRequest https://wolnelektury.pl/media/book/txt/pan-tadeusz.txt
$pages = $requestResault.Content -split [Environment]::NewLine + [Environment]::NewLine | Where-Object { -not [string]::IsNullOrWhiteSpace($_) }

if(Test-Path -Path $outputDirectory){
    Remove-Item $outputDirectory
}

New-Item -Path $outputDirectory -ItemType Directory

for($i = 0 ; $i -le $pages.Count ; $i++){
    $fileIndex = $i + 1
    $filePath = $outputDirectory + "\panTadeusz" + $fileIndex + ".txt"
    echo $pages[$i] > $filePath
}