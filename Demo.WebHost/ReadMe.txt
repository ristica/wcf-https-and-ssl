To run the host over IIS (to use HTTPS), follow these steps:

	1) set the host project to run on Local IIS + create virtual directory
	2) set new application pool
	3) run the pool as admin
	4) set the pool to be the new pool of the app (virtual directory)
	5) test connection (run the solution)

To create new certificate, follow these steps:

	1) double click the server and under IIS tab choose "server certificates"
	2) on the right choose "create self signed certificate"
	3) give the the cert a name
	4) save it in the "Webhosting"
	5) now click on "Default Wbsite"
	6) choose "Bindings"
	7) add "https" binding and select the cert you made in step 3
	8) now click on your virtual directory under Default Website (your host app)
	9) in tab IIS choose "SSL Settings" and set "Require SSL"

That's it!