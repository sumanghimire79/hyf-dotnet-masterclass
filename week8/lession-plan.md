## Deployment

### Create resource group
1. Login into Azure portal
2. Create new resource group

![azure-create-resource-group.png](assets/azure-create-resource-group.png)

3. Fill in resource group name and region

![azure-resource-group-create.png](assets/azure-resource-group-create.png)

4. Click `Review + Create` button on the bottom of the page

![azure-resource-group-rev-create.png](assets/azure-resource-group-rev-create.png "azure-resource-group-rev-create.png")

5. Again, click `Create` button on the bottom of the page

### Create MySQL server
1. Create new MySql Single server. In the search field type ` server`,

![mysql-server.png](assets/mysql-server.png)

2. Click `Create`

![mysql-server-page.png](assets/mysql-server-page.png)

3. Select `Single server` and click `Create`

![mysql-single-server.png](assets/mysql-single-server.png)

4. Fill in required fields. Make sure to select `Sweden Central` for Location. Remember username & password, we will need it later.

![azure-mysql-create-server.png](assets/azure-mysql-create-server.png)

5. Click `Review + Create` on the bottom of the page

6. Click `Create` on the bottom of the page

7. Wait for the server to get deployed (it will take some time)

![azure-mysql-deployment.png](assets/azure-mysql-deployment.png)

### Configure MySQL

1. Select `MySQL` server from the Azure dashboard

![azure-dashboard-mysql.png](assets/azure-dashboard-mysql.png)

2. Select `Connection security`

![azure-mysql-connection-sec.png](assets/azure-mysql-connection-sec.png)

3. Click `Add 0.0.0.0 - 255.255.255.255` from the `Firewall rules` section

![azure-firewall-rules.png](assets/azure-firewall-rules.png)

4. Click `Continue`

![azure-mysql-connection-continue.png](assets/azure-mysql-connection-continue.png "azure-mysql-connection-continue.png")

5. Save the firewall rule

![azure-mysql-firewall-rule-save.png](assets/azure-mysql-firewall-rule-save.png)

### Create database on Azure MySQL Server

1. Select `MySQL` server from the Azure dashboard

![azure-dashboard-mysql.png](assets/azure-dashboard-mysql.png)

2. Copy server name

![azure-mysql-copy.png](assets/azure-mysql-copy.png)

3. Open SQL management tool (MySQL workbench)
4. Create a new connection

![mysql-workbench.png](assets/mysql-workbench.png)

5. Paste the server name from the clipboard into `Hostname` field, enter the username in `username` field and then test the connection. Username should be in `{username}@{mysql_instance_name}` format. You can find this in the `Connection strings` section on Azure.

![mysql-workbench-connection-create.png](assets/mysql-workbench-connection-create.png)

6. Execute DB Schema creation script. Make sure that you have correct DB name. DB name should correspond to the DB name used in the connection string inside of the meal sharing .NET app code.

![mysql-workbench-execute.png](assets/mysql-workbench-execute.png)

### Create App Service
1. Create a new App Service

![app-service.png](assets/app-service.png)

2. Fill in required fields & click `Next: deployment`

![app-service-create.png](assets/app-service-create.png)

3. Enable GitHub actions, fill in the required fields & click `Next: Networking`

![azure-app-service-deployment.png](assets/azure-app-service-deployment.png)

4. Click on `Review + Create` on the bottom of the page
5. Click `Create`

### Update meal-sharing app
1. `App Service` creation process will create the GitHub action deployment workflow inside of the `.github` folder. Github action workflow assumes that your solution is located inside of the repository root, because of this deployment will probably fail (image).

![github-build-failure.png](assets/github-build-failure.png)

To fix this we have to modify GitHub action.
2. Inside of the root repository open & edit GitHub action workflow `.github/workflow`

![Image not found: assets/github-action-edit.png](assets/github-action-edit.png "Image not found: assets/github-action-edit.png")

There are two steps that need to be modified - `Build with dotnet` and `dotnet publish`. Insert the path to the meal-sharing .NET project.

![github-action-modify-steps.png](assets/github-action-modify-steps.png "github-action-modify-steps.png")

Commit and observe the deployment status

![github-action-deployment-fix.png](assets/github-action-deployment-fix.png "github-action-deployment-fix.png")

If fix is successful, GitHub status check should be green

![github-action-deployment-success.png](assets/github-action-deployment-success.png)

3. To ensure that deployment will also contain client bundles, add these lines after the checkout step in the GitHub action workflow:

```yaml
- uses: actions/setup-node@v3
  with:
    node-version: 14
- run: npm install
  working-directory: final/MealsharingNet/ClientApp
- run: npm run build
  working-directory: final/MealsharingNet/ClientApp
```

![github-action-build-frontend.png](assets/github-action-build-frontend.png)

4. Commit changes
5. Add the connection string as environment variable to `App Service`

![azure-app-service-connection.png](assets/azure-app-service-connection.png)
make sure to  enter correct username, password and database name

![azure-app-service-connection-str.png](assets/azure-app-service-connection-str.png)

6. Modify code to read connection string from the environment variable
```csharp
public class Shared
{
  public static string ConnectionString = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MYSQLCONNSTR_MealSharingDb")) ?
 "Server=localhost;Database=final-semih;Uid=root;Pwd=compl3xPassWrd;Convert Zero Datetime=True"   :
 Environment.GetEnvironmentVariable("MYSQLCONNSTR_MealSharingDb");
}
```
7. Commit changes, wait for the deployment to finish - test application!

![azure-app-service-url.png](assets/azure-app-service-url.png)
