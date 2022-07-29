# DEV приложения в ТЛ

## [https://mj-cais.technologica.com/](https://mj-cais.technologica.com/)
## [https://mj-cais-public.technologica.com/](https://mj-cais-public.technologica.com/)
Необходимо е в hosts файла да се съдържа следния запис (и VPN към TL):
```
172.31.12.87 mj-cais.technologica.com
172.31.12.87 mj-cais-public.technologica.com
```

# Builds

|Project|Status|
|--------|------|
|MJ_CAIS.Angular|[![Build Status](https://tfstl.technologica.com/tfs/DefaultCollection/MJ-CAIS/_apis/build/status/MJ_CAIS.Angular)](https://tfstl.technologica.com/tfs/DefaultCollection/MJ-CAIS/_build/latest?definitionId=317)|
|MJ_CAIS.Web|[![Build Status](https://tfstl.technologica.com/tfs/DefaultCollection/MJ-CAIS/_apis/build/status/MJ_CAIS.Web)](https://tfstl.technologica.com/tfs/DefaultCollection/MJ-CAIS/_build/latest?definitionId=319)|
|MJ_CAIS.IdentityServer|[![Build Status](https://tfstl.technologica.com/tfs/DefaultCollection/MJ-CAIS/_apis/build/status/MJ_CAIS.IdentityServer)](https://tfstl.technologica.com/tfs/DefaultCollection/MJ-CAIS/_build/latest?definitionId=318)|


#TODO
1. Портал за граждани -mvc
 - Илия - UX промени, да може да работи от моб.устройства.
 - трябва да се добавят издадените Е-свидетелства, заявени от гражданина
 - feedback - праща мейл на мейл група на МП(+Пепи)
 - да се добави проверка за ЛНЧ, да не успяват да влязат и да подават.
 
2. Служ. портал - mvc (Е-свидетелства, Е-справки) - 2 вида потребители (роля?) administrator - дали може да управлява потребители.
 - трябва да се добавят издадените Е-свидетелства, заявени от служителя
 - да се добави потр.интерфейс за Е-справката * - да се добави таблица, в която да се пишат Е-Справки(журнал) - W_REPORTS, W_REP_PERS_ID
   -- справките са 2 , във заявката да се добавят полета за CallContext: 
    * Е-справка - по идентификатор(тип и стойност) - резултата е pdf (xslt), подписван - 
	MJ_CAIS.Services.CriminalRecordsReportService.GetCriminalRecordsReportPDFAsync
	* Справка за идентификатор на лице - по 3 имена, дата на раждане, точност(YMD, YM, Y), място на раждане - резултата е списък с лица(идентификатори и лични данни) - обект. - 
	MJ_CAIS.Services.CriminalRecordsReportService.PersonIdentifierSearchAsync	
   
 - регистрация на потребители + избор на администрация/ потвърждение в ЦАИС от глобален администратор на ЦАИС
 
3. съхраненение и публичен достъп на pdf-те - само за Свидетелства: гише, ЕС, ЕСС - тук да се мисли за url-a( няма да може лесно да го сменяме, след като системата влезе в употреба)

4. ЦАИС
 - Вътрешни заявки за коригиране на бюлетини
 - Справки за съдимост - да се направи потребителски интерфейс за заявяване и издаване на Справка.(a_report_application, a_reports)
 - в Заявлението за Свидетелство - да се добави информация за док. за самоличност ?
 - в tab-"Проверка в регистри" да се добави визуализация на полето API-SERVICE-CALL-ID
 - HomePage - controller-a
 - журнал на Е-справки за съдимост от служебния достъп - потребителски интерфейс за журнал(роля ЦБС)
 - изтриване на бюлетин***
 
5. Тулчета
 - Процес за свидетелство/справка -корекции и тестване
 
6. Екрис
 - Интеграция с услугата на Цанко - чакаме Цанко
 - Форма за визуализация на съобщения - в момента се работи от Боби
 - Форма за идентификация на лица - в момента се работи Силвия
 
7. Миграция
 - ФББС
 - Потребители от публ. портал
 - БС
 
8. Одит на бюлетини - след статус актуален, всяка промяна в бюлетин му пази версия на данните.(rel-model, xml, pdf), bulletin_statuses_h
!!! предстоящи големи промени по модела на бюлетини bulletin, offence, sanction, probations, decisions
--има xsd, mapping, xslt за текущата версия на бюлетин
--има pdf 