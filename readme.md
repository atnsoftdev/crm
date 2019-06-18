docker run -d -p 18081:8081 -p 18082:18082 --name nexus -v nexus-data:/nexus-data --restart unless-stopped -e NEXUS_CONTEXT=nexus sonatype/nexus3

docker run -p 80:80 -p 443:443 --restart unless-stopped -v /home/jacky/crm/infra/lb/nginx/:/etc/nginx:ro -v /home/jacky/certbot/:/etc/letsencrypt/ -d nginx:1.16.0

docker build -t keycloak-nginx:6.0.1 .

docker run -it --rm -v /home/jacky/certbot:/etc/letsencrypt -v /home/jacky/certbot/www/:/var/www/certbot -w /var/www/certbot certbot/certbot:latest certonly --webroot --webroot-path=/var/www/certbot -d lab-xyz.tk -d "idp.lab-xyz.tk" -d "ci.lab-xyz.tk" --email codelab0601@gmail.com --agree-tos