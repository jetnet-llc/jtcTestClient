# Pre-commit secret guard for jtcTestClient.
# Scans staged changes and blocks the commit if known-leaked creds or any
# non-demo credential value is being introduced. Auth is via Frontegg and the
# only credential that belongs in this repo is the demo login.
$ErrorActionPreference = 'Stop'

$demoEmail = 'demo@jetnet.com'
$demoPass  = 'g846ii2v'

# Literal strings that must never reappear (previously leaked).
$blocked = @(
  'Moz@8765','s$28U7br','i{4374B2','b3v69wba','mmaggi7277',
  'mike@jetnet.com','apiv2prod@jettrack.com','Netjetapi@netjets.com'
)

# Staged files, excluding the guard itself (it necessarily contains the patterns).
$files = git diff --cached --name-only --diff-filter=ACM |
  Where-Object { $_ -and ($_ -notlike '.githooks/*') }

$violations = New-Object System.Collections.Generic.List[string]

# Collect added lines per file.
$added = foreach ($file in $files) {
  git diff --cached --unified=0 --no-color -- $file |
    Where-Object { $_ -match '^\+' -and $_ -notmatch '^\+\+\+' }
}

foreach ($line in $added) {
  $text = $line.Substring(1)

  foreach ($b in $blocked) {
    if ($text.Contains($b)) { $violations.Add("leaked value '$b' -> $($text.Trim())") }
  }

  # EmailAddress = "..." must be the demo email.
  if ($text -match 'EmailAddress\s*=\s*@?"([^"]*)"') {
    if ($Matches[1] -and $Matches[1] -ne $demoEmail) {
      $violations.Add("non-demo email '$($Matches[1])' -> $($text.Trim())")
    }
  }
  # Password = "..." must be the demo password.
  if ($text -match 'Password\s*=\s*@?"([^"]*)"') {
    if ($Matches[1] -and $Matches[1] -ne $demoPass) {
      $violations.Add("non-demo password -> $($text.Trim())")
    }
  }
}

if ($violations.Count -gt 0) {
  Write-Host ""
  Write-Host "COMMIT BLOCKED - credential guard tripped:" -ForegroundColor Red
  $violations | Select-Object -Unique | ForEach-Object { Write-Host "  * $_" -ForegroundColor Yellow }
  Write-Host ""
  Write-Host "This repo should only contain the demo login ($demoEmail / $demoPass)." -ForegroundColor Red
  Write-Host "If this is intentional, bypass with: git commit --no-verify" -ForegroundColor DarkGray
  exit 1
}

exit 0
