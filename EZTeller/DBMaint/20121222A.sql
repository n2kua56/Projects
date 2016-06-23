USE church;

DELETE FROM parms WHERE prmsSrc='ReportPrefix';
INSERT INTO parms (prmsSrc, prmsValue, prmsActive)
  VALUES('ReportPrefix', 'FBC', 'Y');

DELETE FROM parms WHERE prmsSrc='DefaultReportPath';
INSERT INTO parms (prmsSrc, prmsValue, prmsActive)
  VALUES('DefaultReportPath', 'C:\\EZSoftware\\EZTeller\\Reports', 'Y');