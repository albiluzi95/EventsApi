#Material Zhvillimi për krijimin e pipeline në aplikacionin Events Api
##Hyrje
Në një aplikim online mund të ndodhë qe të ketë shumë nën-aplikime, ku secila prej tyre ka një funksionalitet ose shërbim të caktuar. Duke qënë shkakë fakti që në një grupë programuesish të caktuar mundë të ketë raste që të bëhen ndryshime ose shtime të shumta madje edhe brënda një dite, atëherë kompania “Microsoft” ka dale me një shërbim të tyre CI/CD të cilën e kanë shtuar si mundësi në azure devops “ose Team Foundation Service” e cila është një shërbim i pritur që ofron mjete zhvillimi dhe bashkëpunimi. 
Duke qënë ky shkak, ky aplikim që është krijuar që të mundësojë një simulim të një projekti e cila përdor metodat ‘agile’ ose ‘scrum’ për të arritur rezultatin e pritur “produktin final” 

##Teknologjitë e përdorura
Ky aplikacion ësht një shërbim Web Api I cili  është krijuar duke përdorur ASP .NET Core 4.6.1 dhe për pjesen e “Continuous Integration  and Continuous Delivery” është  përdorur azure pipeline.
Një mjet ‘tool’ I vecantë e cila është përdorur për këtë aplikim është ‘SwashBucket ’ I cili na mundëson një ndërfaqje për cdo funksionalitet të caktuar brënda aplimit tone.
Si databasë është përdorur Mlab  si një database remote për shkak të lehtësirave që mundëson.

##Detaje mbi zhvillimin
Kërkesa paraprake që na duhen paraprakisht janë që  të kemi një profil aktivë në Dev Azure dhe të kemi një repository git por mund të shtohet një duke përdorur git e TFC.
Hedhja e kodit në Git
Hapim profilin tonë në  [dev Azure](https://dev.azure.com/<username>)
Krijojmë një projekt të ri dhe detajet që na duhen
![Create new project](https://github.com/albiluzi95/EventsApi/images/newProject.PNG)
 
Ne menyrë që të hedhim kodin tonë duhet të ekzekutojmë një sërë komandash që na mundesojnë edhe hedhjen e kodit tonë lokalë në devops azure.
```git init
git add .
git commit –m “First commit”
git remote add origin https:username@dev.azure.com/username/emer_projekti/_git/emer_projekti
```

#Konfigurimi I pipeline-it në Azure
Pasi është marrë kodi i fundit duke klikuar në menunë ‘Pipelines’ na hapet nje faqe që na mundëson krijimin e sajë dhe klikojmë te 
butoni ‘New pipeline’.
![New Pipeline](https://github.com/albiluzi95/EventsApi/images/newPipeline.PNG)
 
Pasë kësaj duhet të ndiqen një sere procedurash ku fillimisht tregojmë azure çfarë llojë git kemi përdorur e cila në rastin tonë është 
‘Team Foundation Server’
![Where is Code](https://github.com/albiluzi95/EventsApi/images/whereIsCode.PNG)
 
Në rastin tonë ne zgjedhim  Team Foundation Version Control. Pas kësaj zgjedhim llojin e source si më poshtë 
![Select Repository](https://github.com/albiluzi95/EventsApi/images/selectRepository.PNG)
Pasë kësaj na duhet qe të krijojmë pipeline tonë. Azure në këtë llojë rasti ka nje ndërfaqe mjaft solide ku mund të zgjedhim një llojë 
‘templeate’ të gatshëm për krijimin e filet të konfigurmit, por për aplikacione më komplekse ky llojë ‘templeate’ ndahet në disa 
agjentë të cilat shtohen njëri pasë tjetrit duke ruajtur nivelin hiearkik që kanë veprimet.
 ![Selct a templeate](https://github.com/albiluzi95/EventsApi/images/vsts-template-selection.png)
Pasë kësaj neve do na jepet një sere veprimesh “agents” të cilat të grupuara cilësohen si një “job” e cila përfunimisht do na krijojë 
një file azure-pipeline.yml bashkë me setin e te dhënave.
 
 ![Create new Job](https://github.com/albiluzi95/EventsApi/images/createJob.PNG)
Pasi I kemi bërë ‘Save & Queue’ ne kodin tonë në server është shtuar në kodin tonë edhe file I konfigurimit.
 ```
trigger:
- master

pool:
  vmImage: 'windows-latest'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: VSBuild@1
  inputs:
    solution: '$(solution)'
    msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=Package /p:PackageAsSingleFile=true /p:SkipInvalidConfigurations=true /p:PackageLocation="$(build.artifactStagingDirectory)"'
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'

- task: VSTest@2
  inputs:
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'

- task: PublishBuildArtifacts@1
 ```
##Krijimi i pipeline-it për prodhim
	Në faqen ku jemi hapim pjesën e ‘Releases’ dhe shtojmë një pipelinë të ri për atë release.
![Create new release Pipeline](https://github.com/albiluzi95/EventsApi/images/addNewReleasePipeline.PNG)
 
Azure DevOps ka ofruar shumë templeate shabllone të paracaktuara për vendosjen. Disa prej tyre janë si më poshtë
a)	Vendosja e Shërbimit të Azure me Test ose pa Test
b)	Vendosja e Shërbimit të Azure Aplikimit me monitorim të vazhdueshëm
c)	Shërbime Cloud për deployment
d)	Deployment on-Premise
e)	Set shkallë azure VM
f)	Vendosja e politikave të azurit
g)	Funksioni App për Azure

Tani mund te krijojme templeate-in tonë sipas figurës së mëposhtëme:
 ![Create new release Templeate](https://github.com/albiluzi95/EventsApi/images/newReleaseTempleate.PNG)
Pas zgjedhjes së shabllonit, ne mund të shtojmë detyra në mënyrë që të vendosim një mjedise të shumëfishtë si dev, test, UAT etj. Ne 
mund të shtojmë përdorim të veçantë si një afrim lëshimi gjithashtu. Ne mund të vendosim variabla  të ndryshueshme të cilat përdoren 
në përkufizimin e lëshimit. Ne mund t'i ktheni ato shabllone në YAML edhe në një fazë të mëvonshme dhe pastaj të kontrolloni 
kontrollin e versionit nëse dëshironi. Ne shikoojmë që ekzistojnë të gjitha llojet e modeleve të paracaktuara për të bërë ndërtimet 
tuaja. Kështu për shembull, një Gradle build ose Go build, ose doni të bëni disa testime të ngarkesës, ose dëshironi të bëni një 
aplikacion Xamarin për iOS, Ant ndërton, ju e emëroni atë; është gjithçka dhe pjesë e përvojës së jashtme. Azure DevOps ka gjëra të 
tilla si Rivendosja NuGet, Asambletë Test, Ndërtoni zgjidhjen, të gjitha llojet e gjërave që mund të marrim nga zgjedhësi i detyrave.
 ![Create new release Templeate](https://github.com/albiluzi95/EventsApi/images/newReleaseTempleate2.PNG)

 
Ne mund të mundësojmë miratimet e para-vendosjes siç tregohet më poshtë. Tani, çfarë ndodh kur bëra një vendosje dhe dislokimi dështoi?
 Epo, unë mund të zgjedh për të ri-vendosur vendosjen e mëparshme të njohur të qëndrueshme, kështu që unë jam gjithmonë në gjendje të 
 mirë përsëri, e cila është mjaft e dobishme. Pra, kjo është një mënyrë për t'i përcaktuar ato miratime poste përpara se të kalojmë në 
 fazën tjetër të vendosjes sonë.
  ![Add Artifact](https://github.com/albiluzi95/EventsApi/images/preDeployment.PNG)

 
Ne gjithashtu mund të shikojmë në historinë ku mund të gjurmojmë ndryshimet që janë bërë në përcaktimin e lëshimit, i cili është 
gjithashtu shumë i dobishëm nëse bëhet fjalë për pajtueshmërinë dhe sigurinë. Tani ne mund të klonojmë ato ambiente të ndryshme, sepse 
pas dhe ju dëshironi të bëni të njëjtën gjë le të themi dev, si në skenë si në provë ose në prodhim. Kemi nxitje të ndryshme këtu dhe 
një nga nxitësit është se kemi dashur të kemi integrim të vazhdueshëm. Kështu që, çdo herë, ndryshime në repos tonë Git, unë dua të 
shkaktoj pipeline-in  build / Release. Ne gjithashtu mund të planifikojmë gjërat në Azure DevOps.
 ![Add Artifact](https://github.com/albiluzi95/EventsApi/images/scheduleBuild.PNG)
 
Pasi japim save atëherë kz pipeline do inicializohet dhe në fund do na japë një projekt të qëndrueshëm që mund ta shikojmë bashkë me ndryshimet tona.

##Vendime kyçe që janë marrë gjatë zhvillimit
Gjatë krijimit të këtijë pipeline është shumë e rëndësishme të specifikohet se disa pjesë janë lënë menjanë, pasi janë shumë detaje që 
lanë menjanë, pasi edhe pse janë konfigurime të projektit në portal siç janë: krijimi i një web api ne portalin e azure ose 
konfigurime emërtimesh, duke qënë se janë të shumta ato nuk përfshihen në ideologjinë e krijimit të një pipeline duke përdorur azure 
devops.
