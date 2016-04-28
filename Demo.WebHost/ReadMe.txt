- To run the host over IIS (to use HTTPS), follow these steps:

	1) set the host project to run on Local IIS
		a) create virtual directory

- Open IIS Manager and follow these steps:

	2) set new application pool
		a) select "Custom" and set the user for the application pool
		b) save it
	3) set the application pool (created in step 2) to be the new pool of the app (virtual directory)
	4) test connection (run the solution)

- To create new certificate, follow these steps (in IIS Manager):

	5) double click the server (top node) and under IIS tab choose "Server Certificates"
		a) on the right, choose "Create self signed certificate"
		b) give the the cert a name
		c) put it in the "Webhosting"
		d) save it
	---------------------------------
	6) now click on "Default Website"
		a) on the right, choose "Bindings"
		b) add "https" binding and select the cert you made in step 3
		c) save it
	---------------------------------
	7) now click on your virtual directory under Default Website (your host app)
		a) in tab "IIS" choose "SSL Settings" 
		b) set mark on "Require SSL"

That's it!
